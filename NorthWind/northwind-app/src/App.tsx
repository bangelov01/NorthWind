import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import CustomersPage from './pages/CustomersPage'
import CustomerDetailsPage from './pages/CustomerDetailsPage'

function App() {
  return (
    <BrowserRouter>
        <Routes>
          <Route path="/" element={<CustomersPage />} />
          <Route path="/customers/:id" element={<CustomerDetailsPage />} />
        </Routes>
    </BrowserRouter>
  )
}

export default App
