/* eslint-disable react/prop-types */


const StatusMessage = ({ status, successImage, errorImage, isVisible }) => {
  return (
    isVisible && (
      <div className="flex items-center space-x-2">
        <p className={`font-btnFontWeigt font-Helvetica ${status === 'succes' ? 'text-[#858585]' : 'text-[#FF0000]'}`}>
          {status === 'succes' ? 'Success!' : 'Error! Something went wrong.'}
        </p>
        <img
          src={status === 'succes' ? successImage : errorImage}
          alt={status === 'succes' ? 'Checkmark' : 'Error mark'}
          className="w-8 h-8 rounded-full"
        />
      </div>
    )
  );
};

export default StatusMessage;
