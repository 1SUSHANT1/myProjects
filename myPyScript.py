from flask import Flask, request, jsonify, render_template, send_from_directory
import subprocess

app = Flask(__name__)


@app.route("/Images/<path:filename>")
def images(filename):
    return send_from_directory("Images", filename)


@app.route("/")
def home():
    return render_template("index.html")


@app.route("/aboutServer.html")
def about():
    return render_template("aboutServer.html")

@app.route("/index.html")
def index():
	return render_template("index.html")

#@app.route("/.well-known/<path:filename>")
#def wellKnown(filename):
#	return render_template(".well-known/" + filename)	

@app.route("/.well-known/<path:filename>")
def security_txt(filename):
    return send_from_directory("static/.well-known/", filename , mimetype="text/plain")

@app.route("/Projects/<page>")
def projects(page):
    return render_template("Projects/" + page)


@app.route("/SupportingDocuments/<path:filename>")
def supporting_documents(filename):
    return render_template("SupportingDocuments/" + filename)

@app.route("/Scripts/<path:filename>")
def scripts(filename):
    return send_from_directory("Scripts", filename)

@app.route("/admin/<page>")
def admin(page):

	batteryPercentage=subprocess.run(
	["./Executables/adminScripts/batteryPercentage"],
	capture_output=True,
	text=True
	)
	
	chargingStatus=subprocess.run(
	["./Executables/adminScripts/chargingStatus"],
	capture_output=True,
	text=True
	)

	systemUpTime=subprocess.run(
        ["./Executables/adminScripts/systemUpTime"],
        capture_output=True,
        text=True
        )


	return render_template(
		"admin/"+page,
    		batteryPercentage=batteryPercentage.stdout.strip(),
		chargingStatus=chargingStatus.stdout.strip(),
		systemUpTime=systemUpTime.stdout.strip(),
	)


@app.route("/run", methods=["POST"])
def convert():
	user_input = request.json["input"]

	if len(user_input) > 20:
		return jsonify({"error" : "Input too large"}), 400

	result = subprocess.run(
        	["./Executables/NumbersToWords/converter", user_input],
        	capture_output=True,
        	text=True
	)

	return jsonify({
        "output": result.stdout,
        "error": result.stderr
	})


@app.route("/writeSomething", methods=["POST"])
def registerTheInput():

	user_input = request.json["writeSomething"]

	if len(user_input) > 100:
        	return jsonify({"error" : "Input too large"}), 400


	result = subprocess.run(
	["./Executables/WriteSomething/writeLog", user_input],
	capture_output=True,
	text=True
	)

	return "Success"



if __name__ == "__main__":
	app.run(host="0.0.0.0", port=5000,debug=False)
