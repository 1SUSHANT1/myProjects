import requests

with open("/home/user/secrets/API_TOKEN.txt", "r") as AT:
	API_TOKEN = AT.read().strip()
with open("/home/user/secrets/ZONE_ID.txt", "r") as ZI:
	ZONE_ID = ZI.read().strip()
with open("/home/user/secrets/RECORD_ID.txt", "r") as RI:
    	RECORD_ID = RI.read().strip()

url=f"https://api.cloudflare.com/client/v4/zones/{ZONE_ID}/dns_records"

headers={
	"Authorization":f"Bearer {API_TOKEN}",
	"Content-Type": "application/json"
}
try:
	response = requests.get(url,headers=headers)
	response.raise_for_status()
except requests.RequestException as e:
	print(f"Cloudflare API error : {e}")
	exit(1)



try:
    current_ip = requests.get("https://api.ipify.org", timeout=10).text.strip()

except requests.RequestException as e:
    print(f"Could not get public IP: {e}")
    exit(1)

#current_ip = requests.get("https://api.ipify.org").text.strip()
#print(current_ip)

data = response.json()

cloudflare_ip = None

for record in data["result"]:
    if record["type"] == "A" and record["name"] == "sushantadk.com":
        cloudflare_ip = record["content"]
        break

if cloudflare_ip is None:
    print("Could not find A record")
    exit(1)

#cloudflare_ip = data["result"][0]["content"]
#print(cloudflare_ip)

if current_ip != cloudflare_ip:
	print("UPDATE NEEDED")
	
	url = f"https://api.cloudflare.com/client/v4/zones/{ZONE_ID}/dns_records/{RECORD_ID}"

	data = {
		"type": "A",
		"name": "sushantadk.com",
		"content": current_ip,
		"ttl": 1,
		"proxied": True
	
	}


	response = requests.patch(url, headers=headers, json = data)
	print(response.json())



else:
	print("crickets")


#print(response.status_code)
#print(response.json())
