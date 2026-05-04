// src/components/CustomerSearch.tsx
import { useEffect, useState } from 'react'
import { useDebounce } from 'use-debounce'
import { TextField } from '@mui/material'

interface CustomerSearchProps {
  onSearch: (value: string) => void;
}

export default function CustomerSearch({ onSearch }: CustomerSearchProps) {
  const [search, setSearch] = useState('');
  const [debouncedSearch] = useDebounce(search, 300);

  useEffect(() => {
    onSearch(debouncedSearch);
  }, [debouncedSearch, onSearch]);

  return (
    <TextField
      fullWidth
      label="Search by name"
      variant="outlined"
      value={search}
      onChange={e => setSearch(e.target.value)}
      sx={{ mb: 3 }}
    />
  );
}