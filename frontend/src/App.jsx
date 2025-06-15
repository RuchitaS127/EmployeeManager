import React, { useEffect, useState } from "react";
import { Container, Typography, Box } from "@mui/material";
import EmployeeForm from "./components/EmployeeForm";
import EmployeeList from "./components/EmployeeList";
import {
  getAllEmployees,
  createEmployee,
  updateEmployee,
  deleteEmployee,
} from "./services/EmployeeService.js";

const App = () => {
  const [employees, setEmployees] = useState([]);
  const [editingEmployee, setEditingEmployee] = useState(null);

  const loadEmployees = async () => {
    try {
      const response = await getAllEmployees();
      setEmployees(response.data);
    } catch (error) {
      console.error("Failed to fetch employees", error);
    }
  };

  useEffect(() => {
    loadEmployees();
  }, []);

  const handleFormSubmit = async (employee) => {
    try {
      if (employee.id) {
        await updateEmployee(employee.id, employee);
      } else {
        await createEmployee(employee);
      }
      setEditingEmployee(null);
      loadEmployees();
    } catch (error) {
      console.error("Failed to save employee", error);
    }
  };

  const handleEdit = (emp) => {
    setEditingEmployee(emp);
  };

  const handleDelete = async (id) => {
    try {
      await deleteEmployee(id);
      loadEmployees();
    } catch (error) {
      console.error("Failed to delete employee", error);
    }
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Employee Management
      </Typography>

      <EmployeeForm initialData={editingEmployee} onSubmit={handleFormSubmit} />

      <Box mt={4}>
        <EmployeeList
          employees={employees}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      </Box>
    </Container>
  );
};

export default App;
