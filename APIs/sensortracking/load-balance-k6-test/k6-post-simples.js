import http from "k6/http";
import { check, sleep } from "k6";
import { Rate } from "k6/metrics";

export let errorRate = new Rate("errors");

export default function () {
	var url = "http://localhost:64773/api/sensor";
	var params = {
		headers: {			
			"Content-Type": "application/json"
		}
	};

	let data = JSON.stringify({
		"timestamp": 1539112021301,
		"tag": "brasil.sudeste.sensor01",
		"valor": "27"
	});

	check(http.post(url, data, params), {
		"status is 200": (r) => r.status == 200
	}) || errorRate.add(1);

	sleep(1);
};