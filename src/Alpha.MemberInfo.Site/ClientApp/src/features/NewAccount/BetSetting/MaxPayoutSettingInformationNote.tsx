import { t } from 'i18next';

import { productSettingInfoRootUrl } from '@/constants';
import { IconInfo } from '@/features/Shared/Icons';
import useNewAccountStore from "@/store/newAccountStore";

export default function MaxPayoutSettingInformationNote() {
  const theme = useNewAccountStore((state) => state.theme);
  const url = `${productSettingInfoRootUrl}?tabindex=1&theme=${theme}`;
  const tooltipContent = t('MaxPayoutInfoTitle');

  return (
    <a
      href={url}
      target="_blank"
      rel="noopener noreferrer"
      data-tooltip-id="global-tooltip"
      data-tooltip-content={tooltipContent}
    >
      {IconInfo}
    </a>
  );
}
