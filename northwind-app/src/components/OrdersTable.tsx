import { Paper, Table, TableHead, TableRow, TableCell, TableBody, Typography } from '@mui/material'
import type { OrderSummaryDto } from '../types';

interface OrdersTableProps {
  orders: OrderSummaryDto[];
}

export default function OrdersTable({ orders }: OrdersTableProps) {
  if (orders.length === 0) {
    return <Typography color="text.secondary">No orders found.</Typography>;
  }

  return (
    <Paper elevation={2}>
      <Table>
        <TableHead>
          <TableRow sx={{ backgroundColor: 'grey.100' }}>
            <TableCell><strong>Order ID</strong></TableCell>
            <TableCell><strong>Total Value</strong></TableCell>
            <TableCell><strong>Products</strong></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orders.map(o => (
            <TableRow key={o.orderId}>
              <TableCell>{o.orderId}</TableCell>
              <TableCell>${o.totalValue?.toFixed(2)}</TableCell>
              <TableCell>{o.productCount}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}