import { useParams, useNavigate } from 'react-router-dom'
import { useQuery } from '@tanstack/react-query'
import { Box, Typography, Button, CircularProgress, Alert, Divider } from '@mui/material'
import { getCustomerById } from '../api/customers'
import CustomerInfo from '../components/CustomerInfo'
import OrdersTable from '../components/OrdersTable'

export default function CustomerDetailsPage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const { data: customer, isLoading, isError, error } = useQuery({
    queryKey: ['customer', id],
    queryFn: () => getCustomerById(id!),
  });

  return (
    <Box sx={{ maxWidth: 800, mx: 'auto', mt: 4, px: 2 }}>
      <Button
        // startIcon={<ArrowBack />}
        onClick={() => navigate(-1)}
        sx={{ mb: 3 }}
      >
        Back to Customers
      </Button>

      {isLoading && <CircularProgress />}
      {isError && <Alert severity="error">{error.message}</Alert>}

      {!isLoading && !isError && customer && (
        <>
          <CustomerInfo customer={customer} />
          <Divider sx={{ mb: 3 }} />
          <Typography variant="h6" gutterBottom>
            Order History ({customer.orders.length})
          </Typography>
          <OrdersTable orders={customer.orders} />
        </>
      )}
    </Box>
  );
}