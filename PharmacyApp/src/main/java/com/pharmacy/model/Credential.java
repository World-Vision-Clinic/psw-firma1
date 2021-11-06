package com.pharmacy.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "credentials")
public class Credential {
	
	private String hospitalName;
	@Id
	private String hospitalLocalhost;
	private String apiKey;
	
	public String getHospitalName() {
		return hospitalName;
	}
	public void setHospitalName(String hospitalName) {
		this.hospitalName = hospitalName;
	}
	public String getHospitalLocalhost() {
		return hospitalLocalhost;
	}
	public void setHospitalLocalhost(String hospitalLocalhost) {
		this.hospitalLocalhost = hospitalLocalhost;
	}
	public String getApiKey() {
		return apiKey;
	}
	public void setApiKey(String apiKey) {
		this.apiKey = apiKey;
	}
	
	public Credential(String hospitalName, String hospitalLocalhost, String apiKey) {
		super();
		this.hospitalName = hospitalName;
		this.hospitalLocalhost = hospitalLocalhost;
		this.apiKey = apiKey;
	}
	
	public Credential() {}
	
}
