import { useState, useCallback } from 'react'
import { useQuery } from '@tanstack/react-query'
import { Box, Typography, CircularProgress, Alert } from '@mui/material'
import { getCustomers } from '../api/customers'
import CustomerSearch from '../components/CustomerSearch'
import CustomerTable from '../components/CustomersTable'

export default function CustomersPage() {
  const [search, setSearch] = useState('');

  const { data: customers, isLoading, isError, error } = useQuery({
    queryKey: ['customers', search],
    queryFn: () => getCustomers(search),
  });

const handleSearch = useCallback((value: string) => {
  setSearch(value);
}, []);

  return (
    <Box sx={{ maxWidth: 800, mx: 'auto', mt: 4, px: 2 }}>
      <Typography variant="h4" gutterBottom>Customers</Typography>

      <CustomerSearch onSearch={handleSearch} />

      {isLoading && <CircularProgress />}
      {isError && <Alert severity="error">{error.message}</Alert>}
      {!isLoading && !isError && customers && (
        <CustomerTable customers={customers} />
      )}
    </Box>
  );
}