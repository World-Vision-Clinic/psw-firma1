package com.pharmacy.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import com.pharmacy.model.Medicine;
import com.pharmacy.repository.MedicineRepository;

@Service
public class MedicineService {

	@Autowired
    private MedicineRepository medicineRepository;

	public ResponseEntity<List<Medicine>> getAllMedications() {
		return ResponseEntity.ok(medicineRepository.findAll());
	}
	
	
}
