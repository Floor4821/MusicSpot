import mysql.connector
from mysql.connector import Error

def test_connection():
    try:
        connection = mysql.connector.connect(
            host="localhost",
            user="root",
            password="password",
            database="musicspot"
        )

        if connection.is_connected():
            print("success")
        
        connection.close()
    except Error as e:
        print(e)

def insert(query):
        connection = mysql.connector.connect(
            host="localhost",
            user="root",
            password="password",
            database="musicspot"
        )

        cursor = connection.cursor()
        cursor.execute(query)
        connection.commit()

        cursor.close()
        connection.close()

def collect(query):
    return 0

def main():
    print()

if __name__ == "__main__":
    main()

