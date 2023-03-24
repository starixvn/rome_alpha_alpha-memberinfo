import '../styles/globals.scss';
import 'react-tooltip/dist/react-tooltip.css';
import '../i18n/product-setting-info';

import React from 'react';
import ReactDOM from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';

import productSettingInfoRouter from '@/routers/productSettingInfoRouter';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <RouterProvider router={productSettingInfoRouter} />
  </React.StrictMode>,
);
