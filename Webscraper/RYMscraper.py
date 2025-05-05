import requests
import datetime
from bs4 import BeautifulSoup as BS
import undetected_chromedriver as uc
import time
import base64
import json
import math
import random

options = uc.ChromeOptions()
options.add_argument("--no-sandbox")
options.add_argument("--disable-blink-features=AutomationControlled")
driver = uc.Chrome(options=options)

def get_songs(soup):
	length = 0
	processed_songs = []
	songs = soup.find("ul", {"class": "tracks tracklisting"}).find_all("li", {"class": "track"})
	for song in songs:
		try: 
			length = int(song.find("span", {"class": "tracklist_total"}).text.split(":")[1].strip())
		except: 
			try: 
				song_title = song.find("a", {"class": "song"}).text
				processed_songs.append(song_title)
			except Exception as e: 
				print(e)
				continue
	return processed_songs, length

def get_products(length=40):
	products = {
		"Vinyl": None,
		"CD": None,
		"Cassette": None
	}

	for product in (("Vinyl", 50, 30.00), ("CD", 70, 15.00), ("Cassette", 90, 8.00)):
		products[product[0]] = {
			"Stock": random.randint(10, 200),
			"Price": product[2] * math.ceil(length / product[1])
		}

	return products

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

def get_releaseinfo(release_link, artist, format):
	url = release_link
	driver.get(url)
	time.sleep(30)
	release_page = driver.page_source
	soup = BS(release_page, "lxml")

	name = soup.find("div", {"class": "album_title"}).contents[0].strip()
	genres = soup.find("span", {"class": "release_pri_genres"}).text.split(", ")
	try: date_str = soup.find("span", {"class": "issue_year ymd"})["title"]
	except: date_str = None
	songs = get_songs(soup)[0]
	length = get_songs(soup)[1]
	if length != 0: products = get_products(length)
	else: products = get_products()
	try: img_link = "https:" + soup.find('img', attrs={'srcset': True})["srcset"].split(',')[0]
	except: img_link = None


	return {
		"Link": url,
		"Name": name,
		"Artist": artist,
		"Songs": songs,
		"Genres": genres,
		"Release Date": date_str,
		"Release Type": format,
		"Products": products,
		"Cover": img_link
	}

def check_empty(list):
	return all(str(s).strip() == "" for s in list)
    
def main():
	genres = [
		"Ambient",
		"DarkAmbient",
		"ElectronicAmbient",
		"BitMusic",
		"Breakbeat",
		"BubblegumBass",
		"Chillout",
		"DrumAndBass",
		"Dubstep",
		"ElectroIndustrial",
		"Electronic",
		"GlitchHop",
		"Hardcore",
		"House",
		"IDM",
		"Indietronica",
		"Techno",
		"TripHop",
		"Drone",
		"Experimental",
		"Noise",
		"Plunderphonics",
		"AvantFolk",
		"ChamberFolk",
		"FolkRock",
		"IndieFolk",
		"AbstractHipHop",
		"ConsciousHipHop",
		"ExperimentalHipHop",
		"HardcoreHipHop",
		"JazzRap",
		"PopRap",
		"Trap",
		"AvantGardeJazz",
		"CoolJazz",
		"HardBop",
		"JazzFusion",
		"ModalJazz",
		"SmoothJazz",
		"AlternativeMetal",
		"AvantGardeMetal",
		"BlackMetal",
		"DeathMetal",
		"HeavyMetal",
		"ProgressiveMetal",
		"StonerMetal",
		"ThrashMetal",
		"ArtPop",
		"BaroquePop",
		"DancePop",
		"Electropop",
		"GlitchPop",
		"IndiePop",
		"JanglePop",
		"ProgressivePop",
		"PsychedelicPop",
		"Synthpop",
		"ArtPunk",
		"Emo",
		"GothicRock",
		"HardcorePunk",
		"PopPunk",
		"PostHardcore",
		"PostPunk",
		"PunkRock",
		"AlternativeRock",
		"ArtRock",
		"DreamPop",
		"ExperimentalRock",
		"GarageRock",
		"Grunge",
		"HardRock",
		"IndieRock",
		"IndustrialRock",
		"MathRock",
		"NewWave",
		"NoiseRock",
		"PopRock",
		"PostRock",
		"ProgressiveRock",
		"PsychedelicRock",
		"Shoegaze",
		"SlackerRock",
		"Slowcore",
		"NeoSoul",
		"ProgressiveSoul",
		"Soul"
	]
	with open("edited_releases.json", "r", encoding="utf-8") as file:
		releases = json.load(file)
	
	safe = []
	unsafe = []

	for r in releases:
		if r["Genres"] in genres:
			safe.append(r)
		else: unsafe.append(r)

	print(len(safe))
	print(len(unsafe))

	with open("safe.json", "w", encoding="utf-8") as file:
		json.dump(safe, file, indent=4, ensure_ascii=False)

	with open("unsafe.json", "w", encoding="utf-8") as file:
		json.dump(unsafe, file, indent=4, ensure_ascii=False)	
		




if __name__ == "__main__":
    main()