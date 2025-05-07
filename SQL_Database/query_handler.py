from connection import insert

def general_insert(table, columns, values):
    template_values = r"(%s"
    for i in range(1, len(columns.split(","))):
        template_values += r", %s"
    template_values += ")"
    query = f"INSERT INTO {table} {columns} VALUES {template_values}"
    id = insert(query, values)
    return id

def main():
    print()

if __name__ == "__main__":
    main()