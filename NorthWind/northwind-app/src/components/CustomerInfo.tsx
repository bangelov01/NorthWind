import { Box, Typography } from '@mui/material'
import type { CustomerDetailsDto } from '../types';

interface CustomerInfoProps {
  customer: CustomerDetailsDto;
}

export default function CustomerInfo({ customer }: CustomerInfoProps) {
  return (
    <Box sx={{ mb: 4 }}>
      <Typography variant="h5" gutterBottom>{customer.companyName}</Typography>
      {customer.contactName && (
        <Typography variant="body1">Contact: {customer.contactName}</Typography>
      )}
      {customer.contactTitle && (
        <Typography variant="body2" color="text.secondary">{customer.contactTitle}</Typography>
      )}
      {customer.phone && (
        <Typography variant="body2">Phone: {customer.phone}</Typography>
      )}
      {customer.city && customer.country && (
        <Typography variant="body2">
          {[customer.address, customer.city, customer.region, customer.country]
            .filter(Boolean)
            .join(', ')}
        </Typography>
      )}
    </Box>
  );
}