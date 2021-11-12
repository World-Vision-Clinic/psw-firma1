package com.pharmacy.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.pharmacy.dto.ReplyDto;

@Service
public class RepliesService {
	
	private final RestTemplate restTemplate;

    @Autowired
    public RepliesService() {
        this.restTemplate = new RestTemplate();
    }
	
	public boolean sendReply(ReplyDto dto, String apiKey, String hospitalLocalhost) {
		boolean result;
        try {
        	HttpHeaders header = new HttpHeaders();
        	header.set("ApiKey", apiKey);
            ResponseEntity<?> response = restTemplate.exchange(hospitalLocalhost + "/replies/add",
                    HttpMethod.POST, new HttpEntity<>(dto, header), ResponseEntity.class);
            result = response.getStatusCodeValue() == 200;
        } catch (Exception e) {
            e.printStackTrace();
            result = false;
        }
        return result;
	}

    
}
