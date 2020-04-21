import http from "k6/http";
import { check, sleep } from "k6";
import { Trend, Rate } from "k6/metrics";

let listErrorRate = new Rate("Get Sensor errors");
let createErrorRate = new Rate("Post Sensor errors");
let ListTrend = new Trend("Get Sensor");
let CreateTrend = new Trend("Post Sensor");

export let options = {
	thresholds: {
		//"Get Sensor": ["p(95)<500"],
		"Post Sensor": ["p(95)<800"],
	}
};

export default function () {
	//let urlGetSensor = "http://localhost:64773/api/sensor/1";
	let urlPostSensor = "http://localhost:64773/api/sensor";
	let params = {
		headers: {			
			"Content-Type": "application/json"
		}
	};

	// Data for the POST request
	let createSensorData = JSON.stringify({
		"timestamp": 1539112021301,
		"tag": "brasil.sudeste.sensor01",
		"valor" : "27"	
	});

	let requests = {
		"GetSensor": {
			method: "GET",
			url: urlGetSensor,
			params: params
		},
		"PostSensor": {
			method: "POST",
			url: urlPostSensor,
			params: params,
			body: createSensorData
		},
	};

	let responses = http.batch(requests);
	let listResp = responses["GetSensor"];
	let createResp = responses["PostSensor"];

	check(listResp, {
		"status is 200": (r) => r.status === 200
	}) || listErrorRate.add(1);

	ListTrend.add(listResp.timings.duration);

	check(createResp, {
		"status is 201": (r) => r.status === 201
	}) || createErrorRate.add(1);

	CreateTrend.add(createResp.timings.duration);

	sleep(1);
};