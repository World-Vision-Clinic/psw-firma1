package com.pharmacy.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.pharmacy.dto.ObjectionDto;
import com.pharmacy.model.Objection;
import com.pharmacy.service.ObjectionsService;

@RestController
@RequestMapping("/objections")
public class ObjectionsController {

	private static final String API_URL = "http://localhost:43818/";
	@Autowired
	private ObjectionsService objectionService;
	@Autowired
	private JdbcTemplate jdbcTemplate;
	
	public ObjectionsController() {
		
    }
	
	@RequestMapping(value = "/add", method = RequestMethod.POST)
    public ResponseEntity<?> addObjection(@RequestBody ObjectionDto dto) {
        ResponseEntity<?> result = new ResponseEntity<>(HttpStatus.OK);
        
        try {
        	Objection newObjection = new Objection();
    		newObjection.setContent(dto.getContent());
    		newObjection.setIdEncoded(dto.getIdEncoded());
    		newObjection.setHospitalId(API_URL);
    		
    		
    		try {
            	int rows = jdbcTemplate.update(" INSERT INTO objections( "
            			+ " content, hospital_id, id_encoded) "
            			+ "	VALUES ( ?, ?, ?); ",
            	        new Object[] { newObjection.getContent(), "http://localhost:43818", newObjection.getIdEncoded() });
            	
            	System.out.println("upit se izvrsio");
            		
    			}catch(Exception e)
    			{
    				e.printStackTrace();
    				result = ResponseEntity.badRequest().body("Bad request!");
    			}
    		
    		//objectionService.saveObjection(newObjection);
    		
        	//int rows = jdbcTemplate.update("INSERT INTO objections (hospital_localhost, hospital_name, api_key) VALUES(?,?,?)",
        	  //      new Object[] { cred.getHospitalLocalhost(), cred.getHospitalName(), cred.getApiKey() });
        	//return rows == 0?
              //      ResponseEntity.badRequest().body("Bad request!") : new ResponseEntity<>(cred, HttpStatus.OK);
        } catch (Exception e) {
            //e.printStackTrace();
            //result = ResponseEntity.badRequest().body("Bad request!");
        }
        return result;
    }
}
