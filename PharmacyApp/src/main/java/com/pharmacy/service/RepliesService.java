package com.pharmacy.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.pharmacy.dto.ReplyDto;

@Service
public class RepliesService {

	private static final String API_URL = "http://localhost:43818/";
    private final RestTemplate restTemplate;

    @Autowired
    public RepliesService() {
        this.restTemplate = new RestTemplate();
    }

	public boolean sendReply(ReplyDto dto) {
		boolean result;
        try {
        	HttpHeaders header = new HttpHeaders();
        	header.set("ApiKey", "bjJ8hZuV6alsuvzVH8ylL6lfK5tZtBhUK81JDGCUszY");
            ResponseEntity<?> response = restTemplate.exchange(API_URL + "/replies/add",
                    HttpMethod.POST, new HttpEntity<>(dto, header), ResponseEntity.class);
            result = response.getStatusCodeValue() == 200;
        } catch (Exception e) {
            e.printStackTrace();
            result = false;
        }
        return result;
	}
}
