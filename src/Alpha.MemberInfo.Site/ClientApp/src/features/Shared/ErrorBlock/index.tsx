import { Fragment } from 'react';

import { SubMessagesResult } from '@/types';

import styles from './ErrorBlock.module.scss';

export default function ErrorBlock({ error }: { error: SubMessagesResult }) {
  return (
    <div className={styles.container}>
      <div className="msg msg-error">
        {error.message ?? error.ErrMsg}
        {error.subMessages?.length > 0 &&
          error.subMessages.map((subMsg) => (
            <Fragment key={subMsg.message}>
              <br />
              {subMsg.isLive ? (
                  <>
                    <span className="live-badge">Live</span>{' '}
                  </>
                ) : (
                  ''
                )}
              {subMsg.message}
            </Fragment>
          ))}
      </div>
    </div>
  );
}
