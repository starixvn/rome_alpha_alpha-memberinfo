import { t } from 'i18next';
import { MouseEvent } from 'react';

import { productSettingInfoRootUrl } from "@/constants";
import { IconInfo } from '@/features/Shared/Icons';
import useNewAccountStore from "@/store/newAccountStore";

export default function PositionTalkingNote() {
  const theme = useNewAccountStore((state) => state.theme);
  const url = `${productSettingInfoRootUrl}?tabindex=2&theme=${theme}`;

  const handleClick = (e: MouseEvent<HTMLDivElement>) => {
    e.preventDefault();
    window.open(url, '_blank');
    e.stopPropagation();
  };

  const tooltipContent = t('PTInfoTitle');
  return (
    <span onClick={handleClick} aria-hidden data-tooltip-id="global-tooltip" data-tooltip-content={tooltipContent}>
      {IconInfo}
    </span>
  );
}
