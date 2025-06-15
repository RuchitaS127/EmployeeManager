import React from "react";
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, IconButton
} from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";

const EmployeeList = ({ employees, onEdit, onDelete }) => {
  return (
    <TableContainer component={Paper} sx={{ maxWidth: 1000, margin: "auto", mt: 4 }}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>First Name</TableCell>
            <TableCell>Last Name</TableCell>
            <TableCell>Gender</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Date of Birth</TableCell>
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {employees.map((emp) => (
            <TableRow key={emp.id}>
              <TableCell>{emp.firstName}</TableCell>
              <TableCell>{emp.lastName}</TableCell>
              <TableCell>{emp.gender}</TableCell>
              <TableCell>{emp.email}</TableCell>
              <TableCell>{new Date(emp.dateOfBirth).toLocaleDateString()}</TableCell>
              <TableCell align="right">
                <IconButton color="primary" onClick={() => onEdit(emp)}>
                  <Edit />
                </IconButton>
                <IconButton color="error" onClick={() => onDelete(emp.id)}>
                  <Delete />
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
          {employees.length === 0 && (
            <TableRow>
              <TableCell colSpan={6} align="center">No employees found</TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default EmployeeList;
