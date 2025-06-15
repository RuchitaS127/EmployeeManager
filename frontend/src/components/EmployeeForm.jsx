import React, { useState, useEffect } from "react";
import { TextField, Button, Grid, MenuItem, Paper } from "@mui/material";

const genders = ["Male", "Female", "Other"];

const EmployeeForm = ({ initialData, onSubmit }) => {
  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    gender: "",
    email: "",
    dateOfBirth: "",
  });

  useEffect(() => {
    if (initialData) {
      setEmployee(initialData);
    }
  }, [initialData]);

  const handleChange = (e) => {
    setEmployee({ ...employee, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(employee);
  };

  return (
    <Paper elevation={3} style={{ padding: "1.5rem", maxWidth: 600, margin: "auto" }}>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={6}>
            <TextField label="First Name" name="firstName" fullWidth value={employee.firstName} onChange={handleChange} required />
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField label="Last Name" name="lastName" fullWidth value={employee.lastName} onChange={handleChange} required />
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField
              select
              label="Gender"
              name="gender"
              fullWidth
              value={employee.gender}
              onChange={handleChange}
              required
            >
              {genders.map((g) => (
                <MenuItem key={g} value={g}>{g}</MenuItem>
              ))}
            </TextField>
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField label="Email" name="email" type="email" fullWidth value={employee.email} onChange={handleChange} required />
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField
              label="Date of Birth"
              name="dateOfBirth"
              type="date"
              fullWidth
              value={employee.dateOfBirth}
              onChange={handleChange}
              InputLabelProps={{ shrink: true }}
              required
            />
          </Grid>
          <Grid item xs={12}>
            <Button type="submit" variant="contained" color="primary">
              {initialData ? "Update" : "Add"} Employee
            </Button>
          </Grid>
        </Grid>
      </form>
    </Paper>
  );
};

export default EmployeeForm;
