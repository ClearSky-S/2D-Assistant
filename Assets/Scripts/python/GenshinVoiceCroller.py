import requests
from bs4 import BeautifulSoup as bs

page = requests.get("https://genshin-impact.fandom.com/wiki/"+input("input character name(ex: Keqing): ")+"/Voice-Overs/Korean")
soup = bs(page.text, "html.parser")

elements = soup.select('.audio-button >a')

for element in elements:
    print(element.text)
    src = element['href']
    print(src)
    res = requests.get(src)
    open(element.text[6:], "wb").write(res.content)