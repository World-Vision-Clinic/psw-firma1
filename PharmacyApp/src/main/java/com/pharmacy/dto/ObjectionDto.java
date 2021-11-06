package com.pharmacy.dto;

public class ObjectionDto {

	private String content;
	private String idEncoded;
	
	public ObjectionDto(String content, String idEncoded) {
		super();
		this.content = content;
		this.idEncoded = idEncoded;
	}
	
	public ObjectionDto() {
		
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public String getIdEncoded() {
		return idEncoded;
	}

	public void setIdEncoded(String idEncoded) {
		this.idEncoded = idEncoded;
	}
	
	

	
	
}
