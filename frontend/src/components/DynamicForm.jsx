/* eslint-disable react/prop-types */
import { useFormik } from 'formik';

const DynamicForm = ({ fields, onSubmit }) => {
  const initialValues = {};
  fields.map(field => {
    initialValues[field.name] = field.initialValue || '';
  });

  const formik = useFormik({
    initialValues,
    onSubmit: values => {
      onSubmit(values);
    },
    validate: values => {
      const errors = {};
      fields.forEach(field => {
        if (field.required && !values[field.name]) {
          errors[field.name] = 'Verplicht veld';
        }

        // You can add more validation logic based on the field type if needed

        // // Example: Email validation
        // if (field.type === 'email' && values[field.name]) {
        //   if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values[field.name])) {
        //     errors[field.name] = 'Ongeldig e-mailadres';
        //   }
        // }
      });
      return errors;
    },
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      {fields.map(field => (
        <div key={field.name}>
          <label htmlFor={field.name}>{field.label}:</label>
          {field.type === 'select' ? (
            <select
              id={field.name}
              name={field.name}
              onChange={formik.handleChange}
              value={formik.values[field.name]}
            >
              <option value="">Selecteer...</option>
              {field.options.map(option => (
                <option key={option.value} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          ) : (
            <input
              id={field.name}
              name={field.name}
              type={field.type || 'text'}
              onChange={formik.handleChange}
              value={formik.values[field.name]}
            />
          )}
          {formik.errors[field.name] && <div>{formik.errors[field.name]}</div>}
        </div>
      ))}
      <button type="submit">Submit</button>
    </form>
  );
};

export default DynamicForm;
