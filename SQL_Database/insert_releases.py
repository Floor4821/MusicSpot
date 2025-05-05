import json
from query_handler import general_insert

def make_genres(mapping):
    table = "genre"
    columns = "(subgenre, genre)"

    for key in mapping:
        value = mapping[key]
        for i in range(value[0], value[1] + 1):
            print(general_insert(table=table, columns=columns, values=(i, key)))


    
    

def main():
    mapping = {
        1: (1, 3),
        2: (4, 18),
        3: (19, 22),
        4: (23, 26),
        5: (27, 33),
        6: (34, 39),
        7: (40, 47),
        8: (48, 57),
        9: (58, 65),
        10: (66, 84),
        11: (85, 87)
    }

    make_genres(mapping)

if __name__ == "__main__":
    main()