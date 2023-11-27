import styles from './application.module.scss';

/* eslint-disable-next-line */
export interface ApplicationProps {}

export function Application(props: ApplicationProps) {
  return (
    <div className={styles['container']}>
      <h1>Welcome to Application!</h1>
    </div>
  );
}

export default Application;
