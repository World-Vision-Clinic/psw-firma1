package com.pharmacy.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.pharmacy.model.Medicine;


public interface MedicineRepository extends JpaRepository<Medicine, Integer> {

}