import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from './redux/store';
import HomeScreen from './pages/HomeScreen';
import SalesOrderScreen from './pages/SalesOrderScreen';

function App() {
  return (
    <Provider store={store}>
      <Router>
        <div className="min-h-screen bg-gray-50">
          <Routes>
            <Route path="/" element={<HomeScreen />} />
            <Route path="/order/new" element={<SalesOrderScreen />} />
            <Route path="/order/:id" element={<SalesOrderScreen />} />
          </Routes>
        </div>
      </Router>
    </Provider>
  );
}

export default App;
