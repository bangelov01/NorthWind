// src/components/CustomerTable.tsx
import { useNavigate } from 'react-router-dom'
import { Paper, Table, TableHead, TableRow, TableCell, TableBody, Typography } from '@mui/material'
import type { CustomerOverviewDto } from '../types';

interface CustomerTableProps {
  customers: CustomerOverviewDto[];
}

export default function CustomerTable({ customers }: CustomerTableProps) {
  const navigate = useNavigate();

  if (customers.length === 0) {
    return <Typography color="text.secondary">No customers found.</Typography>;
  }

  return (
    <Paper elevation={2}>
      <Table>
        <TableHead>
          <TableRow sx={{ backgroundColor: 'grey.100' }}>
            <TableCell><strong>Name</strong></TableCell>
            <TableCell><strong>Orders</strong></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {customers.map(c => (
            <TableRow
              key={c.customerId}
              hover
              onClick={() => navigate(`/customers/${c.customerId}`)}
              sx={{ cursor: 'pointer' }}
            >
              <TableCell>{c.companyName}</TableCell>
              <TableCell>{c.orderCount}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}