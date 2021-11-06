package com.pharmacy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

@SpringBootApplication
public class PharmacyApplication implements CommandLineRunner {

	@Autowired
	private JdbcTemplate jdbcTemplate;
	
	public static void main(String[] args) {
		SpringApplication.run(PharmacyApplication.class, args);
	}

	@Override
	public void run(String... args) throws Exception {
		/*String sql = "INSERT INTO medications (id, name) VALUES (1, 'Ospamox'); "
				+ "INSERT INTO medications (id, name) VALUES (2, 'Brufen');  "
				+ "INSERT INTO medications (id, name) VALUES (3, 'Aspirin'); ";
	
		int rows = jdbcTemplate.update(sql);
	    if (rows > 0) {
	        System.out.println("A new row has been inserted.");
	    }*/	
	}

}
