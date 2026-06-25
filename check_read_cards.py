import os
import re

cards_dir = r"c:\Users\Joe\Documents\Astro Lupine\Scripts\Cards"

read_keyword_pattern = re.compile(r'AstroLupineKeywords\.Read')
astro_read_var_pattern = re.compile(r'AstroRead(Damage|Block|Cards)Var')

violators = []

for root, _, files in os.walk(cards_dir):
    for file in files:
        if file.endswith('.cs'):
            filepath = os.path.join(root, file)
            with open(filepath, 'r', encoding='utf-8') as f:
                content = f.read()
                
                # Check if it has the Read keyword
                if read_keyword_pattern.search(content):
                    # Check if it lacks AstroReadVar
                    if not astro_read_var_pattern.search(content):
                        violators.append(filepath)

if violators:
    print("Found cards with 'Read' keyword but no 'AstroReadVar':")
    for v in violators:
        print(f" - {v}")
else:
    print("All cards with 'Read' keyword use an 'AstroReadVar'.")
