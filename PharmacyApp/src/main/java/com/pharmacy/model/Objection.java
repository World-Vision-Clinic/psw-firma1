package com.pharmacy.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "objections")
public class Objection {

	
	@Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
	public Integer id;
	private String hospitalId;
	private String content;
	private String idEncoded;
	
	public Objection()
	{
		
	}
	
	public String getHospitalId() {
		return hospitalId;
	}
	public void setHospitalId(String hospitalId) {
		this.hospitalId = hospitalId;
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

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	
}
