package com.pharmacy.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.pharmacy.model.Medicine;
import com.pharmacy.service.MedicineService;

@RestController
@RequestMapping(value = "/medicine")
public class MedicineController {
	 
	@Autowired
	private MedicineService medicineService;
	
	@GetMapping(value = "")
	public ResponseEntity<List<Medicine>> getAllMedications() {
		return medicineService.getAllMedications();
    } 

}

