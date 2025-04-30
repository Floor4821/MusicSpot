import requests
from bs4 import BeautifulSoup as BS
import undetected_chromedriver as uc
import time
import json

options = uc.ChromeOptions()
options.add_argument("--no-sandbox")
options.add_argument("--disable-blink-features=AutomationControlled")
driver = uc.Chrome(version_main=134, options=options)


def get_releases(artist):
	link_dict = {}
	artist = artist.lower().replace(" ", "-")
	types = [("s", "Album"), ("e", "EP")]
	driver.get(f"https://rateyourmusic.com/artist/{artist}")
	if artist == "franz-ferdinand":
		time.sleep(10)
	else:
		time.sleep(3)
	artist_page = driver.page_source
	soup = BS(artist_page, "lxml")
	for t in types:
		link_list = []
		releases_type = soup.find("div", {"id": f"disco_type_{t[0]}"})
		if not releases_type: continue
		releases = releases_type.find_all("div", {"class": "disco_release"})
		for r in releases:
			release_link = "https://rateyourmusic.com" + r.find("a", {"class": "album"})["href"]
			link_list.append(release_link)
			link_dict[t[1]] = link_list

	return link_dict
    
def main():
	with open('Artists.json', 'r', encoding='utf-8') as file:
		artist_dict = json.load(file)

	for artist in artist_dict:
		if artist_dict[artist] == None:
			artist_dict[artist] = get_releases(artist)
		with open('Artists.json', 'w', encoding='utf-8') as file:
			json.dump(artist_dict, file, indent=4)

if __name__ == "__main__":
    main()