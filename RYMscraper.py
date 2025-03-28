import requests
import cloudscraper
from bs4 import BeautifulSoup
url = "https://rateyourmusic.com/~Flora4281"
scraper = cloudscraper.create_scraper()
response = scraper.get(url)
soup_object = BeautifulSoup(response.text)
print(soup_object.get_text())
print("PLEASE JUST COMMIT")