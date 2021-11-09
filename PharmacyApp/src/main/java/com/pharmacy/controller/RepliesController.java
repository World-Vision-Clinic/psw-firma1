package com.pharmacy.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.pharmacy.dto.ReplyDto;
import com.pharmacy.service.RepliesService;


@RestController
@RequestMapping(value = "/reply")
public class RepliesController {

	@Autowired
	private RepliesService repliesService;
	@Autowired
	private JdbcTemplate jdbcTemplate;
	
	@RequestMapping(value = "/add", method = RequestMethod.POST)
    public ResponseEntity<?> addReply(@RequestBody ReplyDto dto) {
        ResponseEntity<?> result;
        try {
        	String hospitalLocalhost = getHospitalIdObjectionId(dto.getObjectionId());
        	String apiKey = getApiKey(dto.getObjectionId());
			boolean action = repliesService.sendReply(dto, apiKey, hospitalLocalhost);
            result = action ? new ResponseEntity<>(HttpStatus.OK) : ResponseEntity.badRequest().body("Bad request!");
            if(action) {
            	jdbcTemplate.update("INSERT INTO replies (content, objection_id) VALUES(?,?)",
        	    new Object[] { dto.getContent(), dto.getObjectionId() });
            }
        } catch (Exception e) {
            e.printStackTrace();
            result = ResponseEntity.badRequest().body("Bad request!");
        }
        return result;
    }

	private String getApiKey(String objectionId) {
		String hospitalLocalhost = getHospitalIdObjectionId(objectionId);
		String sql = "select api_key from credentials where hospital_localhost = ?";
		return jdbcTemplate.queryForObject(sql, String.class, hospitalLocalhost);
	}

	private String getHospitalIdObjectionId(String objectionId) {
		String sql = "select hospital_id from objections where id_encoded = ?";
		return jdbcTemplate.queryForObject(sql, String.class, objectionId);
		
	}
}
