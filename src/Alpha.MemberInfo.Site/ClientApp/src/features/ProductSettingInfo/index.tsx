import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import styles from './ProductSettingInfo.module.scss';
import ProductSettingInformationTable from './ProductSettingInformationTable';
import SettingRemarksMarketBuilder from './SettingRemarksMarket';
import { ProductSetting } from './type';
import ProductSettingInfoLayout from '@/features/ProductSettingInfo/ProductSettingInfoLayout';

type ProductSettingInfoResponse = {
  ProductSettingInfo: ProductSetting[];
  RoleId: number;
  Is789y: boolean;
  ShowBBin: boolean;
  ShowRNGKenoNumberGame: boolean;
};

const getProductsInformationSetting = async (url: string) =>
  axios.get<ProductSettingInfoResponse>(`${url}/GetProductSettingInfo`);

export default function ProductSettingInfo() {
  const { pathname } = useLocation();
  const [data, setData] = useState<ProductSettingInfoResponse>();
  const { t } = useTranslation();
  useEffect(() => {
    getProductsInformationSetting(pathname)
      .then(async (response) => {
        const productsInformationData = response.data;
        setData(productsInformationData);
      })
      .catch(() => {
        setData(undefined);
      });
  }, [pathname]);

  if (!data) {
    return <div />;
  }

  return (
    <ProductSettingInfoLayout>
      <div className={styles.container}>
        <div className={styles.session}>
          <div className={styles.title}>{t('SettingRemarks')}</div>
          <SettingRemarksMarketBuilder
            userRoleId={data?.RoleId}
            Is789y={data?.Is789y}
            showNoteBBin={data.ShowBBin}
            showNoteRNGKenoNumberGame={data.ShowRNGKenoNumberGame}
          />
        </div>
        <div>
          <div className={styles.title}>{t('ProductSettingInformation')}</div>
          <div>{t('ProductGroupTableIntro')}</div>
          <ProductSettingInformationTable ProductSettingInfo={data.ProductSettingInfo} />
        </div>
      </div>
    </ProductSettingInfoLayout>
  );
}
