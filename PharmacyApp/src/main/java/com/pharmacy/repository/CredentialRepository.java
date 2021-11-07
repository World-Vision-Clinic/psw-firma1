package com.pharmacy.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.pharmacy.model.Credential;


public interface CredentialRepository extends JpaRepository<Credential, String>{

}
