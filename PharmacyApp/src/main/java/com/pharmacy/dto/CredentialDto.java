package com.pharmacy.dto;

public class CredentialDto {
	
	private String hospitalName;
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
	
	public CredentialDto(String hospitalName, String hospitalLocalhost, String apiKey) {
		super();
		this.hospitalName = hospitalName;
		this.hospitalLocalhost = hospitalLocalhost;
		this.apiKey = apiKey;
	}
	
	public CredentialDto() {}
}
