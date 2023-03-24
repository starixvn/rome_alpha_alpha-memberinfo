import { createBrowserRouter } from 'react-router-dom';

import ProductSettingInfoPage from '@/features/ProductSettingInfo';

const route = [
  {
    path: '/site-memberinfo/ProductSettingInfo',
    element: <ProductSettingInfoPage />,
  },
];

const productSettingInfoRouter = createBrowserRouter(route);
export default productSettingInfoRouter;
