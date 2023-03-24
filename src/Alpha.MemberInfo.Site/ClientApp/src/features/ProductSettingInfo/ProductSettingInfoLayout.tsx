import { useSearchParams } from 'react-router-dom';
import { useEffect, useState, ReactNode } from 'react';

interface IProps {
  children: ReactNode;
}
export default function ProductSettingInfoLayout({ children }: IProps) {
  const [searchParams] = useSearchParams();
  const [theme, setTheme] = useState('light');
  useEffect(() => {
    const newState = searchParams.get('theme') === 'light' ? 'light' : 'dark';
    setTheme(newState);
  }, [searchParams]);

  return <div data-theme={theme}>{children}</div>;
}
