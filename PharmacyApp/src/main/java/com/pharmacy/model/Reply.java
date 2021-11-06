package com.pharmacy.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "replies")
public class Reply {
	
	@Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
	public Integer id;
	public String objectionId;
	public String content;
	
	public Reply() {}
	
	public Reply(int id, String objectionId, String content) {
		super();
		this.id = id;
		this.objectionId = objectionId;
		this.content = content;
	}

	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
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
