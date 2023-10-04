import i18next, { changeLanguage } from 'i18next';
import { ReactNode, useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';

interface IProps {
  children: ReactNode;
}
export default function ProductSettingInfoLayout({ children }: IProps) {
  const [searchParams] = useSearchParams();
  const [theme, setTheme] = useState('light');
  useEffect(() => {
    const newState = searchParams.get('theme') === 'light' ? 'light' : 'dark';
    setTheme(newState);
    const lng = searchParams.get('lng') ?? 'en-US';
    const { languages } = i18next;
    if (!languages.includes(lng)) {
      changeLanguage('en-US');
    }
  }, [searchParams]);

  return <div data-theme={theme}>{children}</div>;
}
