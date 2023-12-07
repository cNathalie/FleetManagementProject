/* eslint-disable react/prop-types */
import React, { useState, useEffect } from 'react';

const StatusMessage = ({ showCheckMark, successImage, errorImage, duration = 3000 }) => {
  const [isVisible, setIsVisible] = useState(true);
  const [messageType, setMessageType] = useState(showCheckMark === 'success' ? 'success' : 'error');

  useEffect(() => {
    setIsVisible(true);
    setMessageType(showCheckMark);

    const timer = setTimeout(() => {
      setIsVisible(false);
    }, duration);

    return () => clearTimeout(timer);
  }, [showCheckMark, duration]);

  const message = showCheckMark === 'success' ? 'Success!' : 'Error! Something went wrong.';

  return (
    isVisible && (
      <div className="flex items-center space-x-2">
        <p className={`font-btnFontWeigt font-Helvetica ${messageType === 'success' ? 'text-[#858585]' : 'text-[#FF0000]'}`}>
          {message}
        </p>
        <img
          src={messageType === 'success' ? successImage : errorImage}
          alt={messageType === 'success' ? 'Checkmark' : 'Error mark'}
          className="w-8 h-8 rounded-full"
        />
      </div>
    )
  );
};

export default StatusMessage;
