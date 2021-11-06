package com.pharmacy.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.pharmacy.model.Credential;


public interface ICredentialRepository extends JpaRepository<Credential, String>{

}
