package com.pharmacy.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.pharmacy.dto.CredentialDto;
import com.pharmacy.model.Credential;

@RestController
@RequestMapping("/credential")
public class CredentialsController {
	
	@Autowired
	private JdbcTemplate jdbcTemplate;
	
    public CredentialsController() {
        
    }
	
	@RequestMapping(value = "", method = RequestMethod.POST)
    public ResponseEntity<?> addCredential(@RequestBody CredentialDto dto) {
        ResponseEntity<?> result = new ResponseEntity<>(HttpStatus.OK);
        try {
        	Credential cred = new Credential();
        	cred.setHospitalName(dto.getHospitalName());
        	cred.setHospitalLocalhost(dto.getHospitalLocalhost());
        	cred.setApiKey(dto.getApiKey());
        	int rows = jdbcTemplate.update("INSERT INTO credentials (hospital_localhost, hospital_name, api_key) VALUES(?,?,?)",
        	        new Object[] { cred.getHospitalLocalhost(), cred.getHospitalName(), cred.getApiKey() });
        	return rows == 0?
                    ResponseEntity.badRequest().body("Bad request!") : new ResponseEntity<>(cred, HttpStatus.OK);
        } catch (Exception e) {
            //e.printStackTrace();
            //result = ResponseEntity.badRequest().body("Bad request!");
        }
        return result;
    }
}
