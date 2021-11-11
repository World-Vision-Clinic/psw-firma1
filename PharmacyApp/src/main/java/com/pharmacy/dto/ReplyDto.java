package com.pharmacy.dto;

public class ReplyDto {
	
	public String objectionId;
	public String content;
	
	public ReplyDto() {}
	
	public ReplyDto(String objectionId, String content) {
		super();
		this.objectionId = objectionId;
		this.content = content;
	}

	public String getObjectionId() {
		return objectionId;
	}

	public void setObjectionId(String objectionId) {
		this.objectionId = objectionId;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}
	
	
	
}
