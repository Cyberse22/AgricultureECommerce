import { parse, format } from 'date-fns';

export function formatDateForBackend(dateString: string): string {
  // Parse chuỗi dd/mm/yyyy
  const parsedDate = parse(dateString, 'dd/MM/yyyy', new Date());

  // Format lại thành yyyy-mm-dd
  return format(parsedDate, 'yyyy-MM-dd');
}