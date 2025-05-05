from connection import insert

def general_insert(table, columns, values):
    query = f"INSERT INTO {table} {columns} VALUES {values}"
    insert(query)
    return query

def main():
    print()

if __name__ == "__main__":
    main()