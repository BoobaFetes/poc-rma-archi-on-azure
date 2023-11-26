import styles from './react-infra.module.scss';

/* eslint-disable-next-line */
export interface ReactInfraProps {}

export function ReactInfra(props: ReactInfraProps) {
  return (
    <div className={styles['container']}>
      <h1>Welcome to ReactInfra!</h1>
    </div>
  );
}

export default ReactInfra;
