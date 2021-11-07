package com.pharmacy.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.pharmacy.dto.ObjectionDto;
import com.pharmacy.model.Medicine;
import com.pharmacy.model.Objection;
import com.pharmacy.repository.MedicineRepository;
import com.pharmacy.repository.ObjectionRepository;
import com.pharmacy.repository.ObjectionRepositoryImpl;

@Service
public class ObjectionsService {

	private static final String API_URL = "http://localhost:43818/";
    private final RestTemplate restTemplate;
	
    @Autowired
    public ObjectionsService() {
        this.restTemplate = new RestTemplate();
    }
	
	private ObjectionRepositoryImpl objectionRepository = new ObjectionRepositoryImpl();


	public void saveObjection(Objection newObjection) {
		
		objectionRepository.save(newObjection);		
	}
    
}
