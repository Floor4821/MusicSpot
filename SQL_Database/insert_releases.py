import json
import pandas as pd
from query_handler import general_insert
import requests

def make_genres(mapping):
    table = "genre"
    columns = "(subgenre, genre)"

    for key in mapping:
        value = mapping[key]
        for i in range(value[0], value[1] + 1):
            print(general_insert(table, columns, (i, key)))

def handle_releasetype(type):
    types = {
        "Album": 1,
        "EP": 2
    }

    type_id = types[type]

    return type_id

def handle_genre(genre):
    with open("genres.json", "r", encoding="utf-8") as file:
        genres = json.load(file)

    genre_id = genres["subgenres"][genre]

    return genre_id

def handle_image(img_link):
    image = requests.get(img_link).content
    return image

def insert_release_general(release_info):
    table = "`release`" 
    columns = "(cover, releasetype, artist, releasedate, releasename, genreID)"
    values = release_info
    release_id = general_insert(table, columns, values)
    return release_id

def insert_songs(songs, releaseID):
    table = "song"
    columns= "(name, releaseID)"
    for s in songs:
        values = (s, releaseID)
        general_insert(table, columns, values)

def insert_products(products, release_id):
    types = {
        "Vinyl": 1,
        "CD": 2,
        "Cassette": 3
    }

    table = "product"
    columns = "(price, stock, mediatype, releaseID)"
    for p in products:
        mediatype = types[p],
        stock = products[p]["Stock"]
        price = products[p]["Price"]
        values = (price, stock, mediatype[0], release_id)
        general_insert(table, columns, values)

def insert_releases(releases):
    for r in releases[0:8]:
        name = r["Name"]
        artist = r["Artist"]
        genre = handle_genre(r["Genres"])
        date = pd.to_datetime(r["Release Date"])
        type = handle_releasetype(r["Release Type"])
        cover = handle_image(r["Cover"])
        songs = r["Songs"]
        products = r["Products"]

        general_info = (cover, type, artist, date, name, genre)
        release_id = insert_release_general(general_info)
        insert_songs(songs, release_id)
        insert_products(products, release_id)

def list_artists(releases):
    for r in releases:
        print(r["Artist"])

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

    with open("safe.json", "r", encoding="utf-8") as file:
        releases = json.load(file)
    
    insert_releases(releases)

    

if __name__ == "__main__":
    main()