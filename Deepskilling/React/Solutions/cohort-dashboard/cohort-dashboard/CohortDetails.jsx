import React from 'react';
import styles from './CohortDetails.module.css';

const CohortDetails = ({ cohort }) => {
  return (
    <div className={styles.box}>
      <h3 style={{ color: cohort.status === 'ongoing' ? 'green' : 'blue' }}>
        {cohort.title}
      </h3>
      <dl>
        <dt>Status:</dt>
        <dd>{cohort.status}</dd>

        <dt>Start Date:</dt>
        <dd>{cohort.startDate}</dd>

        <dt>End Date:</dt>
        <dd>{cohort.endDate}</dd>
      </dl>
    </div>
  );
};

export default CohortDetails;